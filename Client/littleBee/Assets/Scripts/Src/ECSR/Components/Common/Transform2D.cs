using System.Collections.Generic;
using TrueSync;
using UnityEngine;

namespace Synchronize.Game.Lockstep.Ecsr.Components.Common
{
    /// <summary>
    /// transform组件
    /// 包含位置信息，旋转
    /// </summary>
    public class Transform2D:AbstractComponent
    {
        public TSVector2 Position;
        public FP Radius;
        //public bool ActiveDetection;
        public byte DetectionPriority = 0;
        public TSVector2 Toward;
        #region inner
        public uint CollisionEntityId=0;
        public List<uint> CollisionEntityIds = new List<uint>();
        #endregion
        public Transform2D(TSVector2 pos,FP radius, byte detectionPriority = 0)
        {
            Radius = radius;
            DetectionPriority = detectionPriority;
            Position = pos;

        }
        public Transform2D() { }
        public void SaveCollisionId(uint otherEntityId)
        {
            CollisionEntityId = otherEntityId;
            CollisionEntityIds.Add(otherEntityId);
        }
     
        public void ClearCollisionEntityIds() 
        {
            CollisionEntityId = 0;
            CollisionEntityIds.Clear();
        }


        override public AbstractComponent Clone()
        {
            Transform2D com = new Transform2D(Position,Radius,DetectionPriority);
            com.Enable = Enable;
            com.EntityId = EntityId;
            com.Toward = Toward;
            com.CollisionEntityId = CollisionEntityId;
            com.CollisionEntityIds = new List<uint>(CollisionEntityIds);
            return com;
        }
        public override void CopyFrom(AbstractComponent component)
        {
            EntityId = component.EntityId;
            Transform2D target = component as Transform2D;
            Enable = target.Enable;
            Position = target.Position;
            Radius = target.Radius;
            DetectionPriority = target.DetectionPriority;
            Toward = target.Toward;
            CollisionEntityId = target.CollisionEntityId;
            CollisionEntityIds = new List<uint>(target.CollisionEntityIds);
        }
        public override string ToString()
        {            
            if(DetectionPriority > 0)
                return $"[Transform Id:{EntityId} Pos:{Position.x} {Position.y} CollisionEntityId:({CollisionEntityId})]";
            return $"[Transform Id:{EntityId} Pos:{Position.x} {Position.y}]";
        }

        public override byte[] Serialize()
        {
            using(ByteBuffer buffer = new ByteBuffer())
            {
                 buffer.WriteUInt32(EntityId)
                    .WriteBool(Enable)
                    .WriteInt64(Position.x._serializedValue)
                    .WriteInt64(Position.y._serializedValue)
                    .WriteInt64(Radius._serializedValue)
                    .WriteByte(DetectionPriority)
                    .WriteInt64(Toward.x._serializedValue)
                    .WriteInt64(Toward.y._serializedValue)
                    .WriteUInt32(CollisionEntityId);
                 buffer.WriteInt32(CollisionEntityIds.Count);
                 foreach (var id in CollisionEntityIds)
                 {
                     buffer.WriteUInt32(id);
                 }
                 return buffer.Getbuffer();
            }
        }

        public override AbstractComponent Deserialize(byte[] bytes)
        {
            using (ByteBuffer buffer = new ByteBuffer(bytes))
            {
                EntityId = buffer.ReadUInt32();
                Enable = buffer.ReadBool();
                FP px, py;
                px._serializedValue = buffer.ReadInt64();
                py._serializedValue = buffer.ReadInt64();
                Position = new TSVector2(px,py);
                Radius._serializedValue = buffer.ReadInt64();
                DetectionPriority = buffer.ReadByte();
                FP tx, ty;
                tx._serializedValue = buffer.ReadInt64();
                ty._serializedValue = buffer.ReadInt64();
                Toward = new TSVector2(tx,ty);
                CollisionEntityId = buffer.ReadUInt32();
                int count = buffer.ReadInt32();
                CollisionEntityIds = new List<uint>();
                for (int i = 0; i < count; i++)
                {
                    CollisionEntityIds.Add(buffer.ReadUInt32());
                }
                return this;
            }

        }
    }
}
