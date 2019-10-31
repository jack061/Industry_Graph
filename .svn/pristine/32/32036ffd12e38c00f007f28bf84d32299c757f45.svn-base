
/********************************************************************************
**文 件 名:ModbusGatewayBLL
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:55:26
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JFine.Plugins.IOT.Domain.Models.Modbus;
using JFine.Plugins.IOT.Domain.IRepository.Modbus;
using JFine.Plugins.IOT.Domain.Repository.Modbus;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Data.Common;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;
using HslCommunication;
using HslCommunication.ModBus;

namespace JFine.Plugins.IOT.Busines.Modbus
{	
	/// <summary>
    /// ModbusBLL
	/// </summary>	
    public class ModbusBLL0
	{

        public void Start() 
        {
            ModbusTcpNet busTcpClient = new ModbusTcpNet("192.168.0.20", 502);
            busTcpClient.ConnectServer();
            var value = busTcpClient.ReadInt16("x=3;1");
            var sss = value.Content;
            HslCommunication.OperateResult<byte[]> read = busTcpClient.Read("x=3;1", 3);
            HslCommunication.OperateResult<bool[]> read2 = busTcpClient.ReadDiscrete("0", 3);
            var rrr = read2;
            if (read2.IsSuccess)
            {
                // 共返回20个字节，每个数据2个字节，高位在前，低位在后
                // 在数据解析前需要知道里面到底存了什么类型的数据，所以需要进行一些假设：
                // 前两个字节是short数据类型
                short value1 = busTcpClient.ByteTransform.TransInt16(read.Content, 0);
                // 接下来的2个字节是ushort类型
                ushort value2 = busTcpClient.ByteTransform.TransUInt16(read.Content, 2);
                // 接下来的4个字节是int类型
                int value3 = busTcpClient.ByteTransform.TransInt32(read.Content, 4);
                // 接下来的4个字节是float类型
                //float value4 = busTcpClient.ByteTransform.TransFloat(read.Content, 8);
                // 接下来的全部字节，共8个字节是规格信息
                string speci = Encoding.ASCII.GetString(read.Content, 12, 8);
 
                // 已经提取完所有的数据
            }

        }
    }
}
