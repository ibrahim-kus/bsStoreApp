namespace Entities.DataTransferObjects
{
    //[Serializable]
    //public record BookDto(int id, String Title, decimal Price); // Tanımı  bu şekilde yaparsak xml veri döner ama taglerin okunabilirliği kötü.
    
    //[Serializable] //Gerek kalmıyor.
    public record BookDto
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public decimal Price { get; set; }
    }
}
