﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.Page;

public class PagedViewModel<T> where T : class
{
    public IList<T> Results { get; set; }
    public Page Page { get; set; }

    public PagedViewModel(IList<T> results, Page page)
    {
        Results = results;
        Page = page;
    }
    public PagedViewModel() : this(new List<T>(), new Page())
    {

    }
}
