﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 线路数据应用示例
{
    class InfoSendToCI2
    {
        Pack MyStruct = new Pack(9);
        public byte[] DataSendToCI = new byte[17];

        public InfoSendToCI2()
        {
            SetLogicState(MainWindow.stationElements_1_.Elements);
            MyStruct.Skip();
            SetTrainAccessInfo(AddCIAccess.CITableListDown);
            MyStruct.Skip();
            Array.Copy(MyStruct.buf_, 0, DataSendToCI, 8, 9);
        }

        private void SetTrainAccessInfo(List<CItable> CITable)
        {
            foreach (var item in CITable)
            {
                bool bit = item.IsNonComTrainAccess;
                MyStruct.SetBit(!bit);
            }
        }

        public void SetLogicState(List<线路绘图工具.GraphicElement> Element)
        {
            foreach (var item in Element)
            {
                if (item is Section)
                {
                    bool bitFront = (item as Section).IsFrontLogicOccupy;
                    MyStruct.SetBit(!bitFront);
                    bool bitLast = (item as Section).IsLastLogicOccupy;
                    MyStruct.SetBit(!bitLast);
                }
            }
            foreach (var item in Element)
            {
                if (item is RailSwitch)
                {
                    bool bit = (item as RailSwitch).TrainOccupy == 0 ? false : true;
                    MyStruct.SetBit(bit);
                }
            }
        }
    }
}
