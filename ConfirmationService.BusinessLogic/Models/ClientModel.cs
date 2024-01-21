namespace ConfirmationService.BusinessLogic.Models;

public class ClientModel
{
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsMailSent { get; set; }
        public bool IsEmailConfirm {  get; set; }
}