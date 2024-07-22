var quill = new Quill('#editor', {
    theme: 'snow'
});
document.getElementById('jewelryForm').addEventListener('submit', function (e) {
    // Copy the HTML content from Quill editor to the hidden textarea
    var description = document.getElementById('jewelryDescription');
    description.value = quill.root.innerHTML;
});

$(document).ready(function () {
    // Load old data when errors return
    loadMetalsFromMetalsJson();
    loadGemstoneFromGemstonesJsons();

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
                if (data) {
                    var bidPrice = formatNumber(data.bidPrice);
                    var offerPrice = formatNumber(data.offerPrice);
                    $row.find('.metal-bidprice').val(bidPrice);
                    $row.find('.metal-offerprice').val(offerPrice);
                    calculateTotalMetalPrice();
                } else {
                    $row.find('.metal-bidprice, .metal-offerprice').val('no data');
                    calculateTotalMetalPrice();
                }
            })
            .fail(function () {
                console.error('Failed to fetch metal details.');
                $row.find('.metal-bidprice, .metal-offerprice').val('no data');
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
                if (data) {
                    var gemCost = formatNumber(data.cost);
                    $row.find('.gemstone-cost').val(gemCost);
                    calculateTotalGemstonePrice();
                } else {
                    $row.find('.gemstone-cost').val('no data');
                    calculateTotalGemstonePrice();
                }
            })
            .fail(function () {
                console.error('Failed to fetch gemstone details.');
                $row.find('.gemstone-cost').val('no data');
            });
    });

    // Update sale price on various cost inputs change
    $('#labor-cost, #markup-percent').on('change', function () {
        calculateSalePrice();
    });

    // Handle form submission
    $('#jewelryForm').submit(function (event) {
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
    });

    function addMetalTemplate() {
        var materialTemplate = $('#material-template').html();
        $('#material-container').append(materialTemplate);
        calculateTotalMetalPrice();
    }

    function addGemstoneTemplate() {
        var gemstoneTemplate = $('#gemstone-template').html();
        $('#gemstone-container').append(gemstoneTemplate);
        calculateTotalGemstonePrice();
    }

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
                        if (data) {
                            var bidPrice = formatNumber(data.bidPrice);
                            var offerPrice = formatNumber(data.offerPrice);
                            $row.find('.metal-bidprice').val(bidPrice);
                            $row.find('.metal-offerprice').val(offerPrice);
                            calculateTotalMetalPrice();
                        } else {
                            $row.find('.metal-bidprice, .metal-offerprice').val('no data');
                            calculateTotalMetalPrice();
                        }
                    })
                    .fail(function () {
                        console.error('Failed to fetch metal details.');
                        $row.find('.metal-bidprice, .metal-offerprice').val('no data');
                    });
                $('#material-container').append($row);
                calculateTotalMetalPrice();
            });
        };
    }

    function loadGemstoneFromGemstonesJsons() {
        var gemstonesJson = $('#gemstonesJson').val();
        if (gemstonesJson) {
            var gemstones = JSON.parse(gemstonesJson);
            gemstones.forEach(function (gemstone) {
                var $row = $($('#gemstone-template').html());

                // Set selected value for dropdown
                $row.find('.gemstone-dropdown').val(gemstone.MaterialId);
                // Set value for input fields
                $row.find('.gemstone-weight').val(gemstone.MaterialQuantWeight);

                var selectedMetalId = gemstone.MaterialId;

                $.get(`/Jewelries/Create/GemDetail?id=${selectedMetalId}`)
                    .done(function (data) {
                        if (data) {
                            var gemCost = formatNumber(data.cost);
                            var gemID = formatNumber(data.id);
                            $row.find('.gemstone-cost').val(gemCost);
                            $row.find('.gemstone-id').val(gemID);
                            calculateTotalGemstonePrice();
                        } else {
                            $row.find('.gemstone-cost').val('no data');
                            $row.find('.gemstone-id').val('no data');
                            calculateTotalGemstonePrice();
                        }
                    })
                    .fail(function () {
                        console.error('Failed to fetch gemstone details.');
                        $row.find('.gemstone-cost').val('no data');
                        $row.find('.gemstone-id').val('no data');
                    });
                $('#gemstone-container').append($row);
                calculateTotalGemstonePrice();
            });
        };
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