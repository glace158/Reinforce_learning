execute_process(COMMAND "/home/ubuntu/robot_ws/build/niryo_one_ros/niryo_one_rpi/catkin_generated/python_distutils_install.sh" RESULT_VARIABLE res)

if(NOT res EQUAL 0)
  message(FATAL_ERROR "execute_process(/home/ubuntu/robot_ws/build/niryo_one_ros/niryo_one_rpi/catkin_generated/python_distutils_install.sh) returned error code ")
endif()