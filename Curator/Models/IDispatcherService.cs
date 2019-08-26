﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curator.Models
{
    public interface IDispatcherService
    {
        void Invoke(Action action);

        void InvokeAsync(Action action);
    }
}
