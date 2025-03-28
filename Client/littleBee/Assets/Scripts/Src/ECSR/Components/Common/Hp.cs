using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronize.Game.Lockstep.Ecsr.Components.Common
{
    public class Hp : AbstractComponent
    {
        private int _maxValue;
        public int MaxValue
        {
            get => _maxValue;
        }
        public int Value { private set; get; }
        public Hp(int value,int maxValue)
        {
            _maxValue = maxValue;
            Value = value;
        }
        public Hp() { }
        public void Hurt(int hurtValue)
        {
            if (hurtValue <= Value)
                Value -= hurtValue;
            else
                Value = 0;
        }

        public void AddHp(int addValue)
        {
            if (Value + addValue > _maxValue)
            {
                Value = _maxValue;
            }
            else
            {
                Value += addValue;
            }
            
        }
        public override AbstractComponent Clone()
        {
            Hp hp = new Hp(Value,_maxValue);
            hp.EntityId = EntityId;
            hp.Enable = Enable;
            return hp;
        }
        public override void CopyFrom(AbstractComponent component)
        {
            EntityId = component.EntityId;
            Hp target = component as Hp;
            Enable = target.Enable;
            Value = target.Value;
            _maxValue = target._maxValue;
        }
        public override string ToString()
        {
            return $"[Hp Id:{EntityId} Value:{Value}]";
        }
        public override byte[] Serialize()
        {
            using(ByteBuffer buffer = new ByteBuffer())
            {
                return buffer.WriteUInt32(EntityId)
                    .WriteBool(Enable)
                    .WriteInt32(Value).
                    WriteInt32(_maxValue).Getbuffer();
            }
        }

        public override AbstractComponent Deserialize(byte[] bytes)
        {
            using(ByteBuffer buffer = new ByteBuffer(bytes))
            {
                EntityId = buffer.ReadUInt32();
                Enable = buffer.ReadBool();
                Value = buffer.ReadInt32();
                _maxValue = buffer.ReadInt32();
                return this;
            }
        }
    }
}
