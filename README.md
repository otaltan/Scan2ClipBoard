# Scan2ClipBoard 

Scan2ClipBoard is an android app which recognizes characters from an image and stores them to the clipboard. 
It uses Google's Optical Character Recognition (OCR) to extract the characters
and displays them in a list where you can choose which ones you want to store to the clipboard. 
After that the application closes automatically.

If you like my app you can [buy me a coffee ☕](https://www.paypal.com/donate?hosted_button_id=VKXH39U9NER9W)

## Main features

- Recognizing characters from an image using OCR
- Capture an image or load it from storage
- Rotate the Image by tapping on it
- Display recognized characters or words in a list
- Select from the list what you want to store to the clipboard
- Quick settings tile for a faster access to the app

## Installation

1. Download the apk file [here](https://github.com/otaltan/Scan2ClipBoard/releases). If you don't know how to install apps from unknown sources click [here](https://www.maketecheasier.com/install-apps-from-unknown-sources-android/). 
2. Install it.
3. Try it.
4. Give me feedback.

## How to use

1. Capture an image or load an image.
2. If necessary, rotate the image by tapping on it.
3. Select from the list what you need and click on the button "Copy selected"
   or just click on "copy all".
   
### Example pictures of Scan2ClipBoard

Recognized characters shown in list | Selected list entries | Detected characters from a rotated image
--- | --- | ---
<img src="https://github.com/otaltan/Scan2ClipBoard/blob/master/Example%20Pictures/S2C_1.jpg?raw=true" width="300" height="621"/> | <img src="https://github.com/otaltan/Scan2ClipBoard/blob/master/Example%20Pictures/S2C_2.jpg?raw=true" width="300" height="621"/> | <img src="https://github.com/otaltan/Scan2ClipBoard/blob/master/Example%20Pictures/S2C_3.jpg?raw=true" width="300" height="621"/>

## Compatibility & needed permissions

Scan2ClipBoard requires at leat Android 9.

Scan2ClipBoard needs the following permissions:
- CAMERA -> To capture images
- STORAGE -> To load an image from storage
- INTERNET -> The Google Vision API does an initial library download the first time that it is used
              (except this library is already downloaded). After that it should work offline.

## License

[GNU General Public License version 3](https://www.gnu.org/licenses/gpl-3.0.txt)
