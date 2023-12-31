// Generated by gencpp from file niryo_one_msgs/SetCalibrationCam.msg
// DO NOT EDIT!


#ifndef NIRYO_ONE_MSGS_MESSAGE_SETCALIBRATIONCAM_H
#define NIRYO_ONE_MSGS_MESSAGE_SETCALIBRATIONCAM_H

#include <ros/service_traits.h>


#include <niryo_one_msgs/SetCalibrationCamRequest.h>
#include <niryo_one_msgs/SetCalibrationCamResponse.h>


namespace niryo_one_msgs
{

struct SetCalibrationCam
{

typedef SetCalibrationCamRequest Request;
typedef SetCalibrationCamResponse Response;
Request request;
Response response;

typedef Request RequestType;
typedef Response ResponseType;

}; // struct SetCalibrationCam
} // namespace niryo_one_msgs


namespace ros
{
namespace service_traits
{


template<>
struct MD5Sum< ::niryo_one_msgs::SetCalibrationCam > {
  static const char* value()
  {
    return "34594f1cc2cba58cae4d417628221460";
  }

  static const char* value(const ::niryo_one_msgs::SetCalibrationCam&) { return value(); }
};

template<>
struct DataType< ::niryo_one_msgs::SetCalibrationCam > {
  static const char* value()
  {
    return "niryo_one_msgs/SetCalibrationCam";
  }

  static const char* value(const ::niryo_one_msgs::SetCalibrationCam&) { return value(); }
};


// service_traits::MD5Sum< ::niryo_one_msgs::SetCalibrationCamRequest> should match
// service_traits::MD5Sum< ::niryo_one_msgs::SetCalibrationCam >
template<>
struct MD5Sum< ::niryo_one_msgs::SetCalibrationCamRequest>
{
  static const char* value()
  {
    return MD5Sum< ::niryo_one_msgs::SetCalibrationCam >::value();
  }
  static const char* value(const ::niryo_one_msgs::SetCalibrationCamRequest&)
  {
    return value();
  }
};

// service_traits::DataType< ::niryo_one_msgs::SetCalibrationCamRequest> should match
// service_traits::DataType< ::niryo_one_msgs::SetCalibrationCam >
template<>
struct DataType< ::niryo_one_msgs::SetCalibrationCamRequest>
{
  static const char* value()
  {
    return DataType< ::niryo_one_msgs::SetCalibrationCam >::value();
  }
  static const char* value(const ::niryo_one_msgs::SetCalibrationCamRequest&)
  {
    return value();
  }
};

// service_traits::MD5Sum< ::niryo_one_msgs::SetCalibrationCamResponse> should match
// service_traits::MD5Sum< ::niryo_one_msgs::SetCalibrationCam >
template<>
struct MD5Sum< ::niryo_one_msgs::SetCalibrationCamResponse>
{
  static const char* value()
  {
    return MD5Sum< ::niryo_one_msgs::SetCalibrationCam >::value();
  }
  static const char* value(const ::niryo_one_msgs::SetCalibrationCamResponse&)
  {
    return value();
  }
};

// service_traits::DataType< ::niryo_one_msgs::SetCalibrationCamResponse> should match
// service_traits::DataType< ::niryo_one_msgs::SetCalibrationCam >
template<>
struct DataType< ::niryo_one_msgs::SetCalibrationCamResponse>
{
  static const char* value()
  {
    return DataType< ::niryo_one_msgs::SetCalibrationCam >::value();
  }
  static const char* value(const ::niryo_one_msgs::SetCalibrationCamResponse&)
  {
    return value();
  }
};

} // namespace service_traits
} // namespace ros

#endif // NIRYO_ONE_MSGS_MESSAGE_SETCALIBRATIONCAM_H
