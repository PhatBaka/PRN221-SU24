var quill = new Quill('#editor', {
    theme: 'snow'
});

document.getElementById('jewelryForm').addEventListener('submit', function (e) {
    var description = document.getElementById('jewelryDescription');
    description.value = quill.root.innerHTML;
});

$(document).ready(function () {
    // Load old data when errors return
    loadMetalsFromMetalsJson();
    loadGemstonesFromGemstonesJson();

    // Add new material row
    $('#add-material').on('click', function () {
        addMetalTemplate();
    });

    // Remove material row
    $(document).on('click', '.remove-material', function () {
        $(this).closest('.material-item').remove();
        calculateTotalMetalPrice();
    });

    // Update total metal price on weight change
    $(document).on('change', '.metal-weight', function () {
        calculateTotalMetalPrice();
    });

    // Update metal prices and total metal price on dropdown change
    $(document).on('change', '.metal-dropdown', function () {
        var selectedMetalId = $(this).val();
        var $row = $(this).closest('.material-info');

        $.get(`/Jewelries/Create/MetaDetail?id=${selectedMetalId}`)
            .done(function (data) {
                updateMetalData($row, data);
            })
            .fail(function () {
                console.error('Failed to fetch metal details.');
                updateMetalData($row, null);
            });
    });

    // Add new gemstone row
    $('#add-gemstone').on('click', function () {
        addGemstoneTemplate();
    });

    // Remove gemstone row
    $(document).on('click', '.remove-gemstone', function () {
        $(this).closest('.gemstone-item').remove();
        calculateTotalGemstonePrice();
    });

    // Update total gemstone price on weight change
    $(document).on('change', '.gemstone-weight', function () {
        calculateTotalGemstonePrice();
    });

    // Update gemstone prices and total gemstone price on dropdown change
    $(document).on('change', '.gemstone-dropdown', function () {
        var selectedGemId = $(this).val();
        var $row = $(this).closest('.gemstone-info');

        $.get(`/Jewelries/Create/GemDetail?id=${selectedGemId}`)
            .done(function (data) {
                updateGemstoneData($row, data);
            })
            .fail(function () {
                console.error('Failed to fetch gemstone details.');
                updateGemstoneData($row, null);
            });
    });

    // Update sale price on various cost inputs change
    $('#labor-cost, #markup-percent').on('change', function () {
        calculateSalePrice();
    });

    // Handle form submission
    $('#jewelryForm').submit(function (event) {
        prepareFormSubmission();
    });

    // Utility functions for number formatting and parsing
    function formatNumber(value) {
        return new Intl.NumberFormat('en-US').format(value);
    }

    function parseNumber(value) {
        return parseFloat(value.replace(/,/g, '')) || 0;
    }

    function calculateTotalMetalPrice() {
        let total = 0;
        $('.material-info').each(function () {
            const weight = parseNumber($(this).find('.metal-weight').val());
            const bidPrice = parseNumber($(this).find('.metal-bidprice').val());
            total += weight * bidPrice;
        });
        $('#total-metal-cost').val(formatNumber(total));
        calculateSalePrice();
    }

    function calculateTotalGemstonePrice() {
        let total = 0;
        $('.gemstone-info').each(function () {
            const weight = parseNumber($(this).find('.gemstone-weight').val());
            const bidPrice = parseNumber($(this).find('.gemstone-cost').val());
            total += weight * bidPrice;
        });
        $('#total-gemstone-cost').val(formatNumber(total));
        calculateSalePrice();
    }

    function calculateSalePrice() {
        const totalMetal = parseNumber($('#total-metal-cost').val());
        const totalGemstone = parseNumber($('#total-gemstone-cost').val());
        const laborCost = parseNumber($('#labor-cost').val());
        const markupPercent = parseNumber($('#markup-percent').val());

        let total = (totalMetal + totalGemstone + laborCost) * (1 + markupPercent / 100);
        $('#sale-price').val(formatNumber(total));
    }

    function updateMetalData($row, data) {
        if (data) {
            var bidPrice = formatNumber(data.bidPrice);
            var offerPrice = formatNumber(data.offerPrice);
            $row.find('.metal-bidprice').val(bidPrice);
            $row.find('.metal-offerprice').val(offerPrice);
        } else {
            $row.find('.metal-bidprice, .metal-offerprice').val('no data');
        }
        calculateTotalMetalPrice();
    }

    function updateGemstoneData($row, data) {
        if (data) {
            console.log('data', data);
            var gemCost = formatNumber(data.cost);
            var gemID = data.id;
            var gemImageBase64 = data.image;
            $row.find('.gemstone-cost').val(gemCost);
            $row.find('.gemstone-id').val(gemID);
            if (gemImageBase64) {
                $row.find('.gemstone-image').attr('src', 'data:image/*;base64,' + gemImageBase64);
            } else {
                console.warn('No image data found');
                $row.find('.gemstone-image').attr('src', 'https://mdbootstrap.com/img/Photos/Others/placeholder.jpg');
            } 
        } else {
            $row.find('.gemstone-cost').val('no data');
            $row.find('.gemstone-id').val('no data');
            $row.find('.gemstone-image').attr('src', 'https://mdbootstrap.com/img/Photos/Others/placeholder.jpg');

        }
        calculateTotalGemstonePrice();
    }

    function loadMetalsFromMetalsJson() {
        var metalsJson = $('#metalsJson').val();
        if (metalsJson) {
            var metals = JSON.parse(metalsJson);
            metals.forEach(function (metal) {
                var $row = $($('#material-template').html());

                // Set selected value for dropdown
                $row.find('.metal-dropdown').val(metal.MaterialId);
                // Set value for input fields
                $row.find('.metal-weight').val(metal.MaterialQuantWeight);

                var selectedMetalId = metal.MaterialId;

                $.get(`/Jewelries/Create/MetaDetail?id=${selectedMetalId}`)
                    .done(function (data) {
                        updateMetalData($row, data);
                    })
                    .fail(function () {
                        console.error('Failed to fetch metal details.');
                        updateMetalData($row, null);
                    });
                $('#material-container').append($row);
                calculateTotalMetalPrice();
            });
        }
    }

    function loadGemstonesFromGemstonesJson() {
        var gemstonesJson = $('#gemstonesJson').val();
        if (gemstonesJson) {
            var gemstones = JSON.parse(gemstonesJson);
            gemstones.forEach(function (gemstone) {
                var $row = $($('#gemstone-template').html());

                // Set selected value for dropdown
                $row.find('.gemstone-dropdown').val(gemstone.MaterialId);
                // Set value for input fields
                $row.find('.gemstone-weight').val(gemstone.MaterialQuantWeight);

                var selectedGemId = gemstone.MaterialId;

                $.get(`/Jewelries/Create/GemDetail?id=${selectedGemId}`)
                    .done(function (data) {
                        updateGemstoneData($row, data);
                    })
                    .fail(function () {
                        console.error('Failed to fetch gemstone details.');
                        updateGemstoneData($row, null);
                    });
                $('#gemstone-container').append($row);
                calculateTotalGemstonePrice();
            });
        }
    }

    function prepareFormSubmission() {
        var metals = [];
        $('#material-container .material-info').each(function () {
            var metalId = $(this).find('.metal-dropdown').val();
            var metalWeight = $(this).find('.metal-weight').val();
            if (metalId && metalWeight) {
                metals.push({
                    MaterialId: metalId,
                    MaterialQuantWeight: metalWeight
                });
            }
        });

        var gemstones = [];
        $('#gemstone-container .gemstone-info').each(function () {
            var gemstoneId = $(this).find('.gemstone-dropdown').val();
            var gemstoneWeight = $(this).find('.gemstone-weight').val();
            if (gemstoneId && gemstoneWeight) {
                gemstones.push({
                    MaterialId: gemstoneId,
                    MaterialQuantWeight: gemstoneWeight
                });
            }
        });

        $('#metalsJson').val(JSON.stringify(metals));
        $('#gemstonesJson').val(JSON.stringify(gemstones));
    }

    function addMetalTemplate() {
        $('#material-container').append($('#material-template').html());
    }

    function addGemstoneTemplate() {
        $('#gemstone-container').append($('#gemstone-template').html());
    }
});

function displaySelectedImage(event, elementId) {
    const selectedImage = document.getElementById(elementId);
    const fileInput = event.target;
    const removeButtonContainer = document.getElementById('removeButtonContainer');

    if (fileInput.files && fileInput.files[0]) {
        const reader = new FileReader();

        reader.onload = function (e) {
            selectedImage.src = e.target.result;
            removeButtonContainer.style.display = 'block'; // Show the remove button
        };

        reader.readAsDataURL(fileInput.files[0]);
    }
}

function removeSelectedImage(imageId, fileInputId) {
    const selectedImage = document.getElementById(imageId);
    const fileInput = document.getElementById(fileInputId);
    const removeButtonContainer = document.getElementById('removeButtonContainer');

    // Reset the file input
    fileInput.value = '';

    // Reset the image to the placeholder
    selectedImage.src = 'https://mdbootstrap.com/img/Photos/Others/placeholder.jpg';

    // Hide the remove button
    removeButtonContainer.style.display = 'none';
}

document.querySelectorAll('.price-input').forEach(function (input) {
    input.addEventListener('input', function (e) {
        var value = e.target.value.replace(/,/g, ''); // Remove existing commas
        if (value === "") {
            e.target.value = "";
            return;
        }
        var formattedValue = new Intl.NumberFormat('en-US').format(value);
        e.target.value = formattedValue;
    });

    input.addEventListener('blur', function (e) {
        var value = e.target.value.replace(/,/g, ''); // Remove existing commas
        if (value === "") {
            e.target.value = "0";
            return;
        }
        var formattedValue = new Intl.NumberFormat('en-US').format(value);
        e.target.value = formattedValue;
    });
});