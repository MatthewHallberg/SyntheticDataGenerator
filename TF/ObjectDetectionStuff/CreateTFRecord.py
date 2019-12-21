import tensorflow as tf
from object_detection.utils import dataset_util

flags = tf.compat.v1.flags

# Here is where the output filename of the TFRecord is determined. Change this,
# perhaps to either `training.tfrecord` or `testing.tfrecord`.
flags.DEFINE_string('output_path', 'custom.tfrecord', '')
FLAGS = flags.FLAGS

#TODO: hook to label file
def class_text_to_int(label):
	if label == 'Pikachu':
		return 1
	if label == 'Bulbasaur':
		return 2
	if label == 'Charmander':
		return 3
	else:
		return 4

def create_tfrecord(filename, width, height, labelData):

    filename = str.encode(filename)

    with open(filename, 'rb') as myfile:
		encoded_image_data = myfile.read()
	
    image_format = b'jpeg'
    classes_text = [] # List of string class name of bounding box (1 per box)
    classes = [] # List of integer class id of bounding box (1 per box)
    xmins = [] # List of normalized left x coordinates in bounding box (1 per box)
    xmaxs = [] # List of normalized right x coordinates in bounding box
    ymins = [] # List of normalized top y coordinates in bounding box (1 per box)
    ymaxs = [] # List of normalized bottom y coordinates in bounding box

    chunk_size= 5
    for i in range(0, len(labelData), chunk_size):
	    chunk = labelData[i:i+chunk_size]
	    #remove last newline
	    chunk[4] = chunk[4].rstrip("\n");
	    classes_text.append(chunk[0])
	    classes.append(class_text_to_int(chunk[0]))
	    xmins.append(int(chunk[1]))
	    xmaxs.append(int(chunk[2]))
	    ymins.append(int(chunk[3]))
	    ymaxs.append(int(chunk[4]))
	    
    tfrecord = tf.train.Example(features=tf.train.Features(feature={
        'image/height': dataset_util.int64_feature(int(height)),
        'image/width': dataset_util.int64_feature(int(width)),
        'image/filename': dataset_util.bytes_feature(filename),
        'image/source_id': dataset_util.bytes_feature(filename),
        'image/encoded': dataset_util.bytes_feature(encoded_image_data),
        'image/format': dataset_util.bytes_feature(image_format),
        'image/object/bbox/xmin': dataset_util.float_list_feature(xmins),
        'image/object/bbox/xmax': dataset_util.float_list_feature(xmaxs),
        'image/object/bbox/ymin': dataset_util.float_list_feature(ymins),
        'image/object/bbox/ymax': dataset_util.float_list_feature(ymaxs),
        'image/object/class/text': dataset_util.bytes_list_feature(classes_text),
        'image/object/class/label': dataset_util.int64_list_feature(classes),
    }))
    return tfrecord

def main(_):
    writer = tf.compat.v1.python_io.TFRecordWriter(FLAGS.output_path)

    with open("TrainData/labelData.txt") as fp:
        line = fp.readline()
        while line:
            data = line.split(",")
            # image_id, image_width, image_height, label_name, x1, x2, y1, y2 
            tfrecord = create_tfrecord("TrainData/Images/{}.jpg".format(data[0]), data[1], data[2], data[3:])
            writer.write(tfrecord.SerializeToString())
            line = fp.readline()
        writer.close()

	print("Done.")


if __name__ == '__main__':
     tf.compat.v1.app.run()