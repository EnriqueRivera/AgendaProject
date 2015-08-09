using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class Utils
    {
        public static bool IsOverlappedTime(DateTime event1Start, DateTime event1End, DateTime event2Start, DateTime event2End)
        {
            return (event1Start >= event2Start && event1Start < event2End)
                    || (event1End > event2Start && event1End <= event2End);
        }
    }
}
