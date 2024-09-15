# ImageCompare

Find Matching Images using ImageSharp.Compare and ImageSharp

Given a set of images, will compare them and report the images that match.  A match
is if two images share certain percentage of pixels.  By default it's 97%.

## Examples

### Process all .jpg and .png in Downloads

```bash
$ imgcmp ~/Downloads/*.jpg ~/Downloads/*.png
/Users/bpm/Downloads/IMG_0401-1.jpg and /Users/bpm/Downloads/IMG_0401.jpg match
```

### Process all .jpg and .png in Downloads with 10% Difference

```bash
$ imgcmp --pixel-error-tolerance=10 ~/Downloads/*.jpg ~/Downloads/*.png
/Users/bpm/Downloads/IMG_0401-1.jpg and /Users/bpm/Downloads/IMG_0401.jpg match
/Users/bpm/Downloads/Screenshot-1116-162528.png and /Users/bpm/Downloads/Screenshot-1116-162529.png match
```

## Installation

Single [file](https://github.com/brianmed/ImageCompare/releases) deployment.

## Usage

```
Usage: imgcmp [OPTIONS] FILES
Image Comparison

Options:
      --pixel-error-tolerance=VALUE
                             Pixel Error Tolerance
      --verbose              Print Debugging Messages
  -h, --help                 Show this Message and Exit
```

## Builidng from Source

```
$ git clone https://github.com/brianmed/ImageCompare.git
$ dotnet publish -c Release --self-contained
```
