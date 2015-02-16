namespace DynamicGrid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    /// <summary>
    /// Class definition for MergeEventArgs
    /// </summary>
    public class MergeEventArgs : EventArgs
    {
        public MergeEventArgs(bool isLeft)
        {
            IsLeftMerge = isLeft;
        }

        public bool IsLeftMerge { get; private set; }
    }
}
