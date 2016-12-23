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
        List<string> inputList;
        string lastInput;
        bool Hotkey1 = false;
        bool Hotkey2 = false;
        bool Hotkey3 = false;
        bool HotKeyTrigged = false;
        KeyboardController kb = new KeyboardController();

        public inputProcessor()
        {
            inputList = new List<string>();
            lastInput = null;
        }

        public void processInput(string input)
        {

            
            switch (input)
            {
                case "LeftCtrl":
                    if (!Hotkey1)
                    {
                        Hotkey1 = true;
                    }

                    break;
                case "LeftShift":
                    if (Hotkey1)
                    {
                        Hotkey2 = true;
                    }
                    
                    break;
                case "Q":
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

                case "D1":
                    if (lastInput == "LeftCtrl")
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
                //Thread.Sleep(500);
            }

        }

        public void sendKeys(string key)
        {
           
            kb.TypeString(key);
            

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

