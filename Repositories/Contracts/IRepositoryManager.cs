﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        IBookRepository Book {  get; }  //prop
        //void Save(); //method 
        Task SaveAsync(); //void olunca yazmıyoruz.
    }
}
