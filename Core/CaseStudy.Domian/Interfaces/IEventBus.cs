using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Domian.Interfaces
{
    public interface IEventBus
    {
        void Publish<T>(T eventData);

    }
}
