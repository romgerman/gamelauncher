using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamepadListener
{
    class Gamepad
    {
        public static void StealInputFor<T>() where T : class
        {
            // TODO: We can steal input for class. But i'm not sure about this because we can have opened two dialogs and they are the same class
            // Idk. But we need something like this
        }
    }
}
