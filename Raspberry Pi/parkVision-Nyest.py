# Example code source: https://github.com/raspberrypi/picamera2/blob/main/examples/capture_motion.py

import time
import numpy as np

from picamera2 import Picamera2
from picamera2.encoders import H264Encoder
from picamera2.outputs import FileOutput

lsize = (320, 240)
picam2 = Picamera2()
video_config = picam2.create_video_configuration(main={"size": (1280, 720), "format": "RGB888"},
                                                 lores={"size": lsize, "format": "YUV420"})
picam2.configure(video_config)
encoder = H264Encoder(1000000)
picam2.start()

w, h = lsize
prev = None
encoding = False # True hvis den opfanger bevægelse.
ltime = 0

capture_config = picam2.create_still_configuration()

while True:
    cur = picam2.capture_buffer("lores")
    cur = cur[:w * h].reshape(h, w)
    if prev is not None:
        # Measure pixels differences between current and
        # previous frame
        mse = np.square(np.subtract(cur, prev)).mean()
        if mse > 7:
            if not encoding:
                encoder.output = FileOutput(f"{int(time.time())}.h264")
                picam2.start_encoder(encoder)
                encoding = True
                print("New Motion", mse)
                picam2.switch_mode_and_capture_file(capture_config, "motion_detection_fullres.jpg")
            ltime = time.time()
        else:
            if encoding and time.time() - ltime > 2.0:
                picam2.stop_encoder()
                encoding = False
    prev = cur