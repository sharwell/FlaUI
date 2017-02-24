﻿using System;
using FlaUI.Core;
using FlaUI.Core.AutomationElements.Infrastructure;
using UIA = interop.UIAutomationCore;

namespace FlaUI.UIA3.Converters
{
    public static class AutomationElementConverter
    {
        public static AutomationElement[] NativeArrayToManaged(AutomationBase automation, object nativeElements)
        {
            if (nativeElements == null)
            {
                return new AutomationElement[0];
            }
            var nativeElementsCasted = (UIA.IUIAutomationElementArray)nativeElements;
            var retArray = new AutomationElement[nativeElementsCasted.Length];
            for (var i = 0; i < nativeElementsCasted.Length; i++)
            {
                var nativeElement = nativeElementsCasted.GetElement(i);
                var automationElement = ((UIA3Automation)automation).WrapNativeElement(nativeElement);
                retArray[i] = automationElement;
            }
            return retArray;
        }

        public static AutomationElement NativeToManaged(UIA3Automation automation, UIA.IUIAutomationElement nativeElement)
        {
            return automation.WrapNativeElement(nativeElement);
        }

        public static UIA.IUIAutomationElement ToNative(this AutomationElement automationElement)
        {
            if (automationElement == null)
            {
                return null;
            }
            var basicElement = automationElement.BasicAutomationElement as UIA3BasicAutomationElement;
            if (basicElement == null)
            {
                throw new Exception("Element is not an UIA3 element");
            }
            return basicElement.NativeElement;
        }
    }
}
