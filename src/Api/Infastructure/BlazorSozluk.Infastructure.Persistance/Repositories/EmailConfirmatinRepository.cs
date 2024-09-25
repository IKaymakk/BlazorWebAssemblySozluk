﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Infastructure.Persistance.Repositories
{
    public class EmailConfirmatinRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
    {
        public EmailConfirmatinRepository(BlazorSozlukContext dbContext) : base(dbContext)
        {
        }
    }
}
