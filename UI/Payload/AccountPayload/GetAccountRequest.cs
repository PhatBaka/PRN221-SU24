using BusinessObjects.Enums;
using BusinessObjects;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UI.Payload.AccountPayload
{
    public class GetAccountRequest
    {
        public int AccountId { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? FullName { get; set; }
    }
}
