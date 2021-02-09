# Scan2ClipBoard

Scan2ClipBoard is an android app which recognizes characters from an image and stores them to the clipboard. 
It uses Google's Optical Character Recognition (OCR) to extract the characters
and displays them in a list where you can choose which ones you want to store to the clipboard. 
After that the application closes automatically.

## Main features

- Recognizing characters from an image using OCR
- Capture an image or load it from storage
- Rotate the Image by tapping on it
- Display recognized characters or words in a list
- Select from the list what you want to store to the clipboard
- Quick settings tile for a faster access to the app

## Installation

1. Download the apk file [here](https://github.com/otaltan/Scan2ClipBoard/releases).
2. Install it.
3. Try it.
4. Give me feedback.

## How to use

1. Capture an image or load an image.
2. If necessary, rotate the image by tapping on it.
3. Select from the list what you need and click on the button "Copy selected"
   or just click on "copy all".
   
### Example pictures of Scan2ClipBoard

Recognized characters shown in list | Selected list entries
:-------------------------:|:-------------------------:
![](https://github.com/otaltan/Scan2ClipBoard/blob/master/Example%20Pictures/S2C_1.jpg?raw=true)  |  ![](https://github.com/otaltan/Scan2ClipBoard/blob/master/Example%20Pictures/S2C_2.jpg?raw=true)

## Compatibility & needed permissions

Scan2ClipBoard requires at leat Android 9.

Scan2ClipBoard needs the following permissions:
- CAMERA -> To capture images
- STORAGE -> To load an image from storage
- INTERNET -> The Google Vision API does an initial library download the first time that it is used
              (except this library is already downloaded). After that it should work offline.

## License

[GNU General Public License version 3](https://www.gnu.org/licenses/gpl-3.0.txt)

Please credit me on your projects if you use this app or parts of my code and send me a link to your project. I am interested to see how and what it is used for.
