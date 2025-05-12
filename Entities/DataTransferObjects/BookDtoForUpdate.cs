using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    //public record BookDtoForUpdate(int id, String Title, decimal Price);
    public record BookDtoForUpdate : BookDtoForManipulation
    {
        [Required]
        public int Id { get; set; }
    }
    //{
    //ikinci yol
    //public int id { get; init; }
    //public String Title { get; init; }
    //public decimal Price { get; init; }
    //}
}
