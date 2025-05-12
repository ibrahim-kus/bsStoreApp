namespace Entities.Exceptions
{
    public class PriceOutOfRangeBadRequestException : BadRequestException
    {
        public PriceOutOfRangeBadRequestException() 
            : base("Maximum Price should be less than 1000 and greater then 10.")
        {

        }
    }
}
