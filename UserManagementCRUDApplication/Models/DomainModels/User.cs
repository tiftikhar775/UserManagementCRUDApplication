namespace UserManagementCRUDApplication.Models.DomainModels
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public int PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}

