using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallengeV2.Models;

namespace CodeChallengeV2.Services
{
    public class SensorService : ISensorService
    {
        public Task<SensorPayloadDecoded> DecodePayload(SensorPayload payload)
        {
            SensorPayloadDecoded res = DecodePayload(payload.FPort, payload.Data);
            res.DevEUI = payload.DevEUI;
            res.Time = payload.Time;
            return Task.FromResult(res);
        }

        private SensorPayloadDecoded DecodePayload(string data)
        {
            if (data.Length.Equals(0))
            {
                return new SensorPayloadDecoded();
            }
            int port = DecodeFromLittleEndian(data.Substring(0, 2));
            string nextData = data.Substring(2, data.Length - 2);
            return DecodePayload(port, nextData);
        }
        private SensorPayloadDecoded DecodePayload(int port, string data)
        {
            SensorPayloadDecoded res = new SensorPayloadDecoded();
            switch (port)
            {
                case 20:
                    res = DecodePayload(data.Substring(4, data.Length - 4));
                    res.Battery = DecodeFromLittleEndian(data.Substring(0, 4));
                    break;
                case 40:
                    res = DecodePayload(data.Substring(4, data.Length - 4));
                    int intTempInternal = DecodeFromLittleEndian(data.Substring(0, 4));
                    res.TempInternal = intTempInternal / 100.00;
                    break;
                case 41:
                    res = DecodePayload(data.Substring(4, data.Length - 4));
                    int intTempRed = DecodeFromLittleEndian(data.Substring(0, 4));
                    res.TempRed = intTempRed / 100.00;
                    break;
                case 42:
                    res = DecodePayload(data.Substring(4, data.Length - 4));
                    int intTempBlue = DecodeFromLittleEndian(data.Substring(0, 4));
                    res.TempBlue = intTempBlue / 100.00;
                    break;
                case 43:
                    res = DecodePayload(data.Substring(6, data.Length - 6));
                    int intTempHumidity = DecodeFromLittleEndian(data.Substring(0, 4));
                    res.TempHumidity = intTempHumidity / 100.00;
                    int intHumidity = DecodeFromLittleEndian(data.Substring(4, 2));
                    res.Humidity = intHumidity / 2.00;
                    break;
            }
            return res;
        }

        private int DecodeFromLittleEndian(string littleEndian)
        {
            string reversed = "";
            for (int i = littleEndian.Length - 2; i >= 0; i -= 2)
            {
                reversed = reversed + (littleEndian.Substring(i, 2));
            }
            int result = Convert.ToInt32(reversed, 16);
            return result;
        }
    }
}
