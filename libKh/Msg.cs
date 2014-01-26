﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Kh
{
    public class Msg
    {
        Dictionary<int, int> msgDictionary;
        Stream stream;

        /// <summary>
        /// Create a message object that contains strings
        /// </summary>
        public Msg()
        {
            msgDictionary = new Dictionary<int, int>(32768);
        }

        /// <summary>
        /// Add a message stream
        /// </summary>
        /// <param name="stream">stream to check and add to current file</param>
        public void Add(Stream stream)
        {
            BinaryReader reader = new BinaryReader(stream);

            int unknown = reader.ReadInt32();
            int msgcount = reader.ReadInt32();

            for (int i = 0; i < msgcount; i++)
            {
                int id = reader.ReadInt32();
                int pos = reader.ReadInt32();
                msgDictionary.Add(id, pos);
            }
            this.stream = stream;
        }

        /// <summary>
        /// Get a message's stream from its id
        /// </summary>
        /// <param name="id">id of message to get</param>
        /// <returns>message</returns>
        public Stream Get(int id)
        {
            int pos;
            if (msgDictionary.TryGetValue(id, out pos) == false)
                return null;
            stream.Position = pos;
            return stream;
        }
    }
}
