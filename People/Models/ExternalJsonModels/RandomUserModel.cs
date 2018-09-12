namespace People.Models.ExternalJsonModels
{
    public class RandomUserResultModel
    {
        public RandomUserModel[] Results { get; set; }
    }

    public class RandomUserModel
    {
        public string Gender { get; set; }
        public RandomUserNameModel Name { get; set; }
        public string Email { get; set; }
        public RandomUserLocationModel Location { get; set; }
        public RandomUserPictureModel Picture { get; set; }
    }

    public class RandomUserPictureModel
    {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Thumbnail { get; set; }
    }

    public class RandomUserNameModel
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    public class RandomUserLocationModel
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long Postcode { get; set; }
    }
}
