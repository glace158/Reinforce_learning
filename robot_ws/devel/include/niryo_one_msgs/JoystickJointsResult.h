// Generated by gencpp from file niryo_one_msgs/JoystickJointsResult.msg
// DO NOT EDIT!


#ifndef NIRYO_ONE_MSGS_MESSAGE_JOYSTICKJOINTSRESULT_H
#define NIRYO_ONE_MSGS_MESSAGE_JOYSTICKJOINTSRESULT_H


#include <string>
#include <vector>
#include <memory>

#include <ros/types.h>
#include <ros/serialization.h>
#include <ros/builtin_message_traits.h>
#include <ros/message_operations.h>


namespace niryo_one_msgs
{
template <class ContainerAllocator>
struct JoystickJointsResult_
{
  typedef JoystickJointsResult_<ContainerAllocator> Type;

  JoystickJointsResult_()
    : has_succeeded(0)  {
    }
  JoystickJointsResult_(const ContainerAllocator& _alloc)
    : has_succeeded(0)  {
  (void)_alloc;
    }



   typedef int32_t _has_succeeded_type;
  _has_succeeded_type has_succeeded;





  typedef boost::shared_ptr< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> > Ptr;
  typedef boost::shared_ptr< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> const> ConstPtr;

}; // struct JoystickJointsResult_

typedef ::niryo_one_msgs::JoystickJointsResult_<std::allocator<void> > JoystickJointsResult;

typedef boost::shared_ptr< ::niryo_one_msgs::JoystickJointsResult > JoystickJointsResultPtr;
typedef boost::shared_ptr< ::niryo_one_msgs::JoystickJointsResult const> JoystickJointsResultConstPtr;

// constants requiring out of line definition



template<typename ContainerAllocator>
std::ostream& operator<<(std::ostream& s, const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> & v)
{
ros::message_operations::Printer< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >::stream(s, "", v);
return s;
}


template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator==(const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator1> & lhs, const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator2> & rhs)
{
  return lhs.has_succeeded == rhs.has_succeeded;
}

template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator!=(const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator1> & lhs, const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator2> & rhs)
{
  return !(lhs == rhs);
}


} // namespace niryo_one_msgs

namespace ros
{
namespace message_traits
{





template <class ContainerAllocator>
struct IsMessage< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >
  : TrueType
  { };

template <class ContainerAllocator>
struct IsMessage< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> const>
  : TrueType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >
  : TrueType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> const>
  : TrueType
  { };

template <class ContainerAllocator>
struct HasHeader< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> const>
  : FalseType
  { };


template<class ContainerAllocator>
struct MD5Sum< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >
{
  static const char* value()
  {
    return "c46039d3cf9aba9e4c57d528de8250ae";
  }

  static const char* value(const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator>&) { return value(); }
  static const uint64_t static_value1 = 0xc46039d3cf9aba9eULL;
  static const uint64_t static_value2 = 0x4c57d528de8250aeULL;
};

template<class ContainerAllocator>
struct DataType< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >
{
  static const char* value()
  {
    return "niryo_one_msgs/JoystickJointsResult";
  }

  static const char* value(const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator>&) { return value(); }
};

template<class ContainerAllocator>
struct Definition< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >
{
  static const char* value()
  {
    return "# ====== DO NOT MODIFY! AUTOGENERATED FROM AN ACTION DEFINITION ======\n"
"# result\n"
"int32 has_succeeded\n"
;
  }

  static const char* value(const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator>&) { return value(); }
};

} // namespace message_traits
} // namespace ros

namespace ros
{
namespace serialization
{

  template<class ContainerAllocator> struct Serializer< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >
  {
    template<typename Stream, typename T> inline static void allInOne(Stream& stream, T m)
    {
      stream.next(m.has_succeeded);
    }

    ROS_DECLARE_ALLINONE_SERIALIZER
  }; // struct JoystickJointsResult_

} // namespace serialization
} // namespace ros

namespace ros
{
namespace message_operations
{

template<class ContainerAllocator>
struct Printer< ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator> >
{
  template<typename Stream> static void stream(Stream& s, const std::string& indent, const ::niryo_one_msgs::JoystickJointsResult_<ContainerAllocator>& v)
  {
    s << indent << "has_succeeded: ";
    Printer<int32_t>::stream(s, indent + "  ", v.has_succeeded);
  }
};

} // namespace message_operations
} // namespace ros

#endif // NIRYO_ONE_MSGS_MESSAGE_JOYSTICKJOINTSRESULT_H