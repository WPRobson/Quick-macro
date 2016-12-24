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
        int lastInput;
        bool Hotkey1 = false;
        bool Hotkey2 = false;
        bool HotKeyTrigged = false;
        KeyboardController kb = new KeyboardController();

        public inputProcessor()
        {
            inputList = new List<int>();
            lastInput = 0;
        }

        public void processInput(int input)
        {

            
            switch (input)
            {
                case 162:
                    if (!Hotkey1)
                    {
                        Hotkey1 = true;
                    }

                    break;
                case 160:
                    if (Hotkey1)
                    {
                        Hotkey2 = true;
                    }
                    
                    break;
                case 81:
                    if (Hotkey1 && Hotkey2)
                    {
                        toggleHotkeyState();
                       // removeHotkeysFromList();
                        Hotkey1 = false;
                        Hotkey2 = false;
                    }
                    else
                    {
                        inputList.Add(input);
                    }
                    break;

                case 49:
                    if (lastInput == 162)
                    {
                        //inputList.RemoveRange(inputList.Count - 2, 2);
                        performMacro();
                    }
                    break;
                default:

                    if (HotKeyTrigged)
                    {
                        inputList.Add(input);
                    }
                    break;

                    
            }
            lastInput = input;
        }


        private void performMacro()
        {
            Thread.Sleep(1000);
            foreach (var item in inputList)
            {
                sendKeys(item);
            }
        }

        private void sendKeys(int key)
        {
            HenoohDeviceEmulator.Native.VirtualKeyCode keyCode = (HenoohDeviceEmulator.Native.VirtualKeyCode)key;
            kb.Type(keyCode);
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
                inputList.Clear();
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

