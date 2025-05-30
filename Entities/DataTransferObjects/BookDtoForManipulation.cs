﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract record BookDtoForManipulation
    {
        [Required(ErrorMessage = "Title is required field.")]
        [MinLength(2, ErrorMessage = "Title must consist of at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "Title must consist of at maximum 50 characters.")]
        public String title { get; set; }

        [Required(ErrorMessage = "Title is required field.")]
        [Range(10,1000)]
        public decimal Price { get; set; }
    }
}
