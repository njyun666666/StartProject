﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartProject.Services.IServices
{
    public interface ILogService
    {
        public void Add(int status, string message);
    }
}
