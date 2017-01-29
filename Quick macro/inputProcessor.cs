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
        static key defaultKey = new key(0, true);
        key[] keyBuffer = new key[3]{ defaultKey, defaultKey , defaultKey};
        bool removeFirstQ = false;

        public inputProcessor()
        {
            inputList = new List<int>();
            KeyList = new List<key>();
            lastInput = 0;
        }

        public bool processInput(int input, bool keyDown)
        {
            key newKey = new key(input, keyDown);

         

            if (!HotKeyTrigged)
            {
                addKeyToBuffer(newKey);

                if (checkKeyBuffer())
                {
                    return false;
                }
            }
            else
            {
                addKeyToBuffer(newKey);
                if (!checkKeyBuffer())
                {
                    KeyList.Add(newKey);

                }

            }


            return HotKeyTrigged ? true : false;
           
        }


        private void addKeyToBuffer(key inKey)
        {
            keyBuffer[arrayHeadIndex] = inKey;
            arrayHeadIndex++;

            if (arrayHeadIndex >2)
            {
                arrayHeadIndex = 0;
            }
        }

        private bool checkKeyBuffer()
        {
            for (int i = 0; i < keyBuffer.Length; i++)
            {
                if (keyBuffer[i].keycode == 162)
                {
                    int nextIndex1 = i + 1;
                    int nextIndex2 = i + 2;
                    if (nextIndex1 ==3)
                    {
                        nextIndex1 = 0;
                        nextIndex2 = 1;
                    }
                    if (nextIndex2 == 3)
                    {
                        nextIndex2 = 0;
                    }
                    if (keyBuffer[nextIndex1].keycode == 49)
                    {
                        if (!HotKeyTrigged)
                        {
                            performMacro();
                        }
                        
                        
                        return false;
                    }

                    if (keyBuffer[nextIndex1].keycode == 160)
                    {
                       
                        if (keyBuffer[nextIndex2].keycode == 81 )
                        {
                            removeFirstQ = true;
                            toggleHotkeyState();
                            return true;
                        }
                    }
                }
            }
            return false;
        }



        private void performMacro()
        {

            keyBuffer[0] = defaultKey;
            keyBuffer[1] = defaultKey;
            keyBuffer[2] = defaultKey;

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

