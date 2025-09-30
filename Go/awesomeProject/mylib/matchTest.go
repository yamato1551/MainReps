package mylib

import (
	"fmt"
	"regexp"
)

func MatchLesson() {
	match, _ := regexp.MatchString("a([a-z0-9]+)e", "apple")
	fmt.Println(match) // true

	r := regexp.MustCompile("a([a-z0-9]+)e")
	ms := r.FindString("apple")
	fmt.Println(ms)

	s := "/view/test"
	r2 := regexp.MustCompile("^/(edit|save|view)/([a-zA-Z0-9])+$")
	fs := r2.FindString(s)
	fmt.Println(fs)
	fss := r2.FindStringSubmatch(s)
	fmt.Println(fss, fss[0], fss[1], fss[2])
}
