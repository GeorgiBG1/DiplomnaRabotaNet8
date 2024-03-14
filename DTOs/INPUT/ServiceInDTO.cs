namespace DTOs.INPUT
{
    public class ServiceInDTO
    {
        public string Images { get; set; } = "none";
        public virtual IEnumerable<IFormFile> ImageFiles {  get; set; } 
    }
}
