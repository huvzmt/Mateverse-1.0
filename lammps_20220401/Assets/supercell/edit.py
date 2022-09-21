df = open('./ice15.data','r').readlines()

num_of_atoms = int(df[1].split()[0])

xyz = df[16:16+num_of_atoms]
xyz_out = open('ice15.xyz','w+')
for line in xyz:
    line_temp = line.split()
    if line_temp[2] == '8':
        line_temp[2] = '2'
    xyz_out.write(' '.join(line_temp)+'\n')
xyz_out.close()
