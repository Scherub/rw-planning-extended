using System;
using System.Runtime.Serialization;

namespace PlanningExtended.Gui
{
    [Serializable]
    internal class GuiException : Exception
    {
        public GuiException() { }
        public GuiException(string message) : base(message) { }
        public GuiException(string message, Exception inner) : base(message, inner) { }
        protected GuiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
