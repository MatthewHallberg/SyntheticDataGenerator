import tensorflow as tf
from object_detection.utils import dataset_util
from object_detection.utils import label_map_util

#set label map dict
label_map_dict = label_map_util.get_label_map_dict("../labelmap.pbtxt")

def create_tfrecord(filename, width, height, labelData):

    filename = filename.encode('utf8')

    with open(filename, 'rb') as myfile:
        encoded_image_data = myfile.read()
	
    height = int(height)
    width = int(width)
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
        classes_text.append(chunk[0].encode('utf8'))
        classes.append(label_map_dict[chunk[0]])
        xmins.append(float(chunk[1]) / width)
        xmaxs.append(float(chunk[2]) / width)
        ymins.append(float(chunk[3]) / height)
        ymaxs.append(float(chunk[4]) / height)
	    
    tfrecord = tf.train.Example(features=tf.train.Features(feature={
        'image/height': dataset_util.int64_feature(height),
        'image/width': dataset_util.int64_feature(width),
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

def read_file(type):
    writer = tf.python_io.TFRecordWriter("../" + type + ".record")

    with open("../" + type + ".txt") as fp:
        line = fp.readline()
        while line:
            data = line.split(",")
            # image_id, image_width, image_height, label_name, x1, x2, y1, y2 
            tfrecord = create_tfrecord("../" + type + "/{}.jpg".format(data[0]), data[1], data[2], data[3:])
            writer.write(tfrecord.SerializeToString())
            line = fp.readline()
        writer.close()
        print(type + " record complete.")

def main(_):
    read_file("train")
    read_file("test")

if __name__ == '__main__':
    tf.app.run()