using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronize.Game.Lockstep.Ecsr.Components.Common
{
    public class Treasure : AbstractComponent
    {
        public int EffectConfig { private set; get; }
        public int EffectTypeId { private set; get; }
        public Treasure(int id)
        {
            EffectConfig = id;
        }
        public Treasure() { }
        public override AbstractComponent Clone()
        {
            Treasure treasure = new Treasure(EffectConfig);
            treasure.EntityId = EntityId;
            treasure.Enable = Enable;
            return treasure;
        }
        public override void CopyFrom(AbstractComponent component)
        {
            EntityId = component.EntityId;
            Treasure target = component as Treasure;
            Enable = target.Enable;
            EffectConfig = target.EffectConfig;
        }
        public override string ToString()
        {
            return $"[Treasure EntityId:{EntityId} TreasureId:{EffectConfig}]";
        }
        public override byte[] Serialize()
        {
            using(ByteBuffer buffer = new ByteBuffer())
            {
                return buffer.WriteUInt32(EntityId)
                    .WriteBool(Enable)
                    .WriteInt32(EffectConfig).
                    WriteInt32(EffectTypeId).Getbuffer();
            }
        }

        public override AbstractComponent Deserialize(byte[] bytes)
        {
            using(ByteBuffer buffer = new ByteBuffer(bytes))
            {
                EntityId = buffer.ReadUInt32();
                Enable = buffer.ReadBool();
                EffectConfig = buffer.ReadInt32();
                EffectTypeId = buffer.ReadInt32();
                return this;
            }
        }
    }
}