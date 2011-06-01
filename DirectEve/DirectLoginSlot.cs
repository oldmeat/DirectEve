﻿// ------------------------------------------------------------------------------
//   <copyright from='2010' to='2015' company='THEHACKERWITHIN.COM'>
//     Copyright (c) TheHackerWithin.COM. All Rights Reserved.
// 
//     Please look in the accompanying license.htm file for the license that 
//     applies to this source code. (a copy can also be found at: 
//     http://www.thehackerwithin.com/license.htm)
//   </copyright>
// -------------------------------------------------------------------------------
namespace DirectEve
{
    using System.Text.RegularExpressions;
    using global::DirectEve.PySharp;

    public class DirectLoginSlot : DirectObject
    {
        private PyObject _pySlot;

        internal DirectLoginSlot(DirectEve directEve, PyObject pySlot) : base(directEve)
        {
            _pySlot = pySlot;
        }

        /// <summary>
        ///   Return the character id associated with this slot
        /// </summary>
        public long CharId
        {
            get { return (long) _pySlot.Attribute("sr").Attribute("charid"); }
        }

        /// <summary>
        ///   Return the character name associated with this slot
        /// </summary>
        public string CharName
        {
            get
            {
                var strip = new Regex(@"<(.|\n)*?>");
                return strip.Replace((string) _pySlot.Attribute("sr").Attribute("smallcaption").Attribute("text"), string.Empty);
            }
        }

        /// <summary>
        ///   Activate this slot, this could make it main slot (if its slot 1 or 2) or login the character (slot 0)
        /// </summary>
        /// <returns></returns>
        public bool Activate()
        {
            return DirectEve.ThreadedCall(_pySlot.Attribute("OnClick"), _pySlot);
        }
    }
}