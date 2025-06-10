namespace CodePulseAPI.Models.Domain
{
    public class Categories
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }
        //moi quan he nhieu -n giuwa categories vaf blogpost
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}
