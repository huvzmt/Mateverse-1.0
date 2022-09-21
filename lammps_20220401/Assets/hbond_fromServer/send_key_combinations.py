from pykeyboard import *
from pymouse import *
import time
import sys

m = PyMouse()
k = PyKeyboard()

actions = sys.argv[1:]

m.click(1470, 500)
time.sleep(0.5)

if len(actions) == 1:
	# k.tap_key(actions[0])
	k.press_key(k.up_key)
else:
	for act in actions:
		if len(act) > 1:
			k.press_key(act[0])
			k.press_key(act[1])
			time.sleep(0.2)
			k.release_key(act[0])
			k.release_key(act[1])
		else:
			k.tap_key(act)

time.sleep(1)
m.click(490, 5)