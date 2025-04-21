namespace WebApplication1.Models
{
    public class UserInfo
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime DateofBirth { get; set; }
        public int Department { get; set; }
    }

    public class Sample
    {
        string hello = "";
        public Sample(string abc)
        {
            hello = abc;
        }

        public Sample(string abc,string cde)
        {
            hello = abc;
        }

        public int add()
        {
            return 1;
        }
    }
}
