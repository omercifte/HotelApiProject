namespace HotelProject.WebUI.Dtos.FollowersDto
{
    public class ResultLinkedlnFollowersDto
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Basic_Info basic_info { get; set; }
    }

    public class Basic_Info
    {
        public int follower_count { get; set; }
        public int connection_count { get; set; }
    }



}
