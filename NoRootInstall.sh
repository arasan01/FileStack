#!/bin/bash
instPath=~/.fs

chmod 755 fs

mkdir $instPath
cp fs $instPath/

cat << EOS >> ~/.bashrc
alias fs='~/.fs/fs'
EOS

echo Install Complete !
