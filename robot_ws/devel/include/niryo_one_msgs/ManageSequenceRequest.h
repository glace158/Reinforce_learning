// Generated by gencpp from file niryo_one_msgs/ManageSequenceRequest.msg
// DO NOT EDIT!


#ifndef NIRYO_ONE_MSGS_MESSAGE_MANAGESEQUENCEREQUEST_H
#define NIRYO_ONE_MSGS_MESSAGE_MANAGESEQUENCEREQUEST_H


#include <string>
#include <vector>
#include <memory>

#include <ros/types.h>
#include <ros/serialization.h>
#include <ros/builtin_message_traits.h>
#include <ros/message_operations.h>

#include <niryo_one_msgs/Sequence.h>

namespace niryo_one_msgs
{
template <class ContainerAllocator>
struct ManageSequenceRequest_
{
  typedef ManageSequenceRequest_<ContainerAllocator> Type;

  ManageSequenceRequest_()
    : cmd_type(0)
    , sequence_id(0)
    , sequence()  {
    }
  ManageSequenceRequest_(const ContainerAllocator& _alloc)
    : cmd_type(0)
    , sequence_id(0)
    , sequence(_alloc)  {
  (void)_alloc;
    }



   typedef int32_t _cmd_type_type;
  _cmd_type_type cmd_type;

   typedef int32_t _sequence_id_type;
  _sequence_id_type sequence_id;

   typedef  ::niryo_one_msgs::Sequence_<ContainerAllocator>  _sequence_type;
  _sequence_type sequence;





  typedef boost::shared_ptr< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> > Ptr;
  typedef boost::shared_ptr< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> const> ConstPtr;

}; // struct ManageSequenceRequest_

typedef ::niryo_one_msgs::ManageSequenceRequest_<std::allocator<void> > ManageSequenceRequest;

typedef boost::shared_ptr< ::niryo_one_msgs::ManageSequenceRequest > ManageSequenceRequestPtr;
typedef boost::shared_ptr< ::niryo_one_msgs::ManageSequenceRequest const> ManageSequenceRequestConstPtr;

// constants requiring out of line definition



template<typename ContainerAllocator>
std::ostream& operator<<(std::ostream& s, const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> & v)
{
ros::message_operations::Printer< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >::stream(s, "", v);
return s;
}


template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator==(const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator1> & lhs, const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator2> & rhs)
{
  return lhs.cmd_type == rhs.cmd_type &&
    lhs.sequence_id == rhs.sequence_id &&
    lhs.sequence == rhs.sequence;
}

template<typename ContainerAllocator1, typename ContainerAllocator2>
bool operator!=(const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator1> & lhs, const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator2> & rhs)
{
  return !(lhs == rhs);
}


} // namespace niryo_one_msgs

namespace ros
{
namespace message_traits
{





template <class ContainerAllocator>
struct IsMessage< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >
  : TrueType
  { };

template <class ContainerAllocator>
struct IsMessage< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> const>
  : TrueType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct IsFixedSize< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> const>
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >
  : FalseType
  { };

template <class ContainerAllocator>
struct HasHeader< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> const>
  : FalseType
  { };


template<class ContainerAllocator>
struct MD5Sum< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "da644ff8e5065e5598b50f9f1180dd4c";
  }

  static const char* value(const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator>&) { return value(); }
  static const uint64_t static_value1 = 0xda644ff8e5065e55ULL;
  static const uint64_t static_value2 = 0x98b50f9f1180dd4cULL;
};

template<class ContainerAllocator>
struct DataType< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "niryo_one_msgs/ManageSequenceRequest";
  }

  static const char* value(const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator>&) { return value(); }
};

template<class ContainerAllocator>
struct Definition< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >
{
  static const char* value()
  {
    return "int32 cmd_type\n"
"\n"
"int32 sequence_id\n"
"niryo_one_msgs/Sequence sequence\n"
"\n"
"\n"
"================================================================================\n"
"MSG: niryo_one_msgs/Sequence\n"
"int32 id\n"
"string name\n"
"string description\n"
"\n"
"int32 created\n"
"int32 updated\n"
"\n"
"string blockly_xml\n"
"string python_code\n"
;
  }

  static const char* value(const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator>&) { return value(); }
};

} // namespace message_traits
} // namespace ros

namespace ros
{
namespace serialization
{

  template<class ContainerAllocator> struct Serializer< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >
  {
    template<typename Stream, typename T> inline static void allInOne(Stream& stream, T m)
    {
      stream.next(m.cmd_type);
      stream.next(m.sequence_id);
      stream.next(m.sequence);
    }

    ROS_DECLARE_ALLINONE_SERIALIZER
  }; // struct ManageSequenceRequest_

} // namespace serialization
} // namespace ros

namespace ros
{
namespace message_operations
{

template<class ContainerAllocator>
struct Printer< ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator> >
{
  template<typename Stream> static void stream(Stream& s, const std::string& indent, const ::niryo_one_msgs::ManageSequenceRequest_<ContainerAllocator>& v)
  {
    s << indent << "cmd_type: ";
    Printer<int32_t>::stream(s, indent + "  ", v.cmd_type);
    s << indent << "sequence_id: ";
    Printer<int32_t>::stream(s, indent + "  ", v.sequence_id);
    s << indent << "sequence: ";
    s << std::endl;
    Printer< ::niryo_one_msgs::Sequence_<ContainerAllocator> >::stream(s, indent + "  ", v.sequence);
  }
};

} // namespace message_operations
} // namespace ros

#endif // NIRYO_ONE_MSGS_MESSAGE_MANAGESEQUENCEREQUEST_H