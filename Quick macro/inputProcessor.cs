using HenoohDeviceEmulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Quick_macro
{
    class inputProcessor
    {
        List<int> inputList;
        List<key> KeyList;
        int lastInput;
        bool Hotkey1 = false;
        bool Hotkey2 = false;
        bool HotKeyTrigged = false;
        KeyboardController kb = new KeyboardController();
        int arrayHeadIndex = 0;
        static key defaultKey = new key(0, true, DateTime.Now);
        key[] keyBuffer = new key[3]{ defaultKey, defaultKey , defaultKey};
        bool removeFirstUPKey;

        public inputProcessor()
        {
            inputList = new List<int>();
            KeyList = new List<key>();
            lastInput = 0;
        }

        public bool processInput(key newKey)
        {
            KeyList.Add(newKey);
            return HotKeyTrigged ? true : false;
        }


    private void performMacro()
        {
            
            Thread.Sleep(800);
            kb.Up(HenoohDeviceEmulator.Native.VirtualKeyCode.LCONTROL);
            foreach (var item in KeyList)
            {
                sendKeys(item);
            }
        }

        private void sendKeys(key key)
        {
            HenoohDeviceEmulator.Native.VirtualKeyCode keyCode = (HenoohDeviceEmulator.Native.VirtualKeyCode)key.keycode;
            //kb.Type(keyCode);
            if (key.keyDown)
            {
                kb.Down(keyCode);
                 
            }
            else
            {
                kb.Up(keyCode);
            }

            
            
        }

        private void toggleHotkeyState()
        {
            if (HotKeyTrigged)
            {
                HotKeyTrigged = false;
            }
            else
            {
                HotKeyTrigged = true;
                
                KeyList.Clear();
            }
        }

        private void removeHotkeysFromList()
        {
            if (inputList.Count >0)
            {
                inputList.RemoveRange(inputList.Count - 3, 3);
            }
        }

        

        



    }
    }

