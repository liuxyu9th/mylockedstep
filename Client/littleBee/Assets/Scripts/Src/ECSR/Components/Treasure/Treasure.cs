using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronize.Game.Lockstep.Ecsr.Components.Common
{
    public class Treasure : AbstractComponent
    {
        public int treasureCfgId { get; private set; }

        public Treasure(int cfgId)
        {
            treasureCfgId = cfgId;
        }
        public Treasure() { }
        public override AbstractComponent Clone()
        {
            Treasure treasure = new Treasure(treasureCfgId);
            treasure.EntityId = EntityId;
            treasure.Enable = Enable;
            treasure.treasureCfgId = treasureCfgId;
            return treasure;
        }
        public override void CopyFrom(AbstractComponent component)
        {
            EntityId = component.EntityId;
            Treasure target = component as Treasure;
            Enable = target.Enable;
            treasureCfgId = target.treasureCfgId;
        }
        public override string ToString()
        {
            return $"[Treasure EntityId:{EntityId} TreasureId:{treasureCfgId}]";
        }
        public override byte[] Serialize()
        {
            using(ByteBuffer buffer = new ByteBuffer())
            {
                return buffer.WriteUInt32(EntityId)
                    .WriteBool(Enable)
                    .WriteInt32(treasureCfgId).Getbuffer();
            }
        }

        public override AbstractComponent Deserialize(byte[] bytes)
        {
            using(ByteBuffer buffer = new ByteBuffer(bytes))
            {
                EntityId = buffer.ReadUInt32();
                Enable = buffer.ReadBool();
                treasureCfgId = buffer.ReadInt32();
                return this;
            }
        }
    }
}