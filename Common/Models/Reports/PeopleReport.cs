namespace Common.Models.Reports
{
    public class PeopleReport
    {
        public long Id { get; set; }

        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public string PictureMedium { get; set; }

        public string PoemContent { get; set; }
        public double PoemDistance { get; set; }
    }
}
