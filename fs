#!/bin/bash

THISPRO=$(basename $0)

function usage_exit(){
  cat <<_EOS_ 1>&2
NAME
    $THISPRO -- File Stack 持ち運びたいファイルは手元から

SYNOPSIS
    $THISPRO push argv1 argv2 ...
    $THISPRO [ pop | pall | ls | reset ]
    
DESCRIPTION
    push    : ファイルを格納する
    pop     : 最新のファイルの格納順にファイルを放出する
    pall    : 格納されているファイルを全て放出する
    ls      : リストを表示
    reset   : スタックリストを消去する
_EOS_
  exit 1
}

path=~/.fstack
if [ ! -e ${path} ];then
    touch ${path}
fi

case "$1" in
    "push" )
    if [ ! -z $2 ];then
        for i in `seq 2 ${#}`
        do
            if [ ! -e ${2} ];then
                continue
            fi
            abspath=$(cd $(dirname ${2}) && pwd)/$(basename ${2})
            echo "${abspath}" >> ${path}
            shift
        done
    fi
    ;;
    "pop" )
        pstring=`tail -n 1 ${path}`
        if [ ! -z $pstring ];then
            mv ${pstring} ./
            tem=`sed -e '$d' ${path}`
            echo "$tem" >| ${path}
            filename=`basename $pstring`
            echo "drop ${filename}"
        fi


    ;;
    "pall" )
        while :
        do
            pstring=`tail -n 1 ${path}`
            if [ ! -z $pstring ];then
                mv ${pstring} ./
                tem=`sed -e '$d' ${path}`
                echo "$tem" >| ${path}
                filename=`basename $pstring`
            else
                echo "drop all"
                break
            fi
        done
    ;;
    "ls" )
    cat ${path}

    ;;
    "reset" )
        rm -f ${path}
        touch ${path}

    ;;
    * )
    usage_exit

    ;;
esac
