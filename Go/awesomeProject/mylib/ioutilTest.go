package mylib

import (
	"bytes"
	"fmt"
	"io"
)

func IoutilLesson() {
	// content, err := os.ReadFile("main.go")
	// if err != nil {
	// 	log.Fatal(err)
	// }
	// fmt.Println(string(content))

	// if err := os.WriteFile("ioutil_temp.go", content, 0666); err != nil {
	// 	log.Fatalln(err)
	// }

	r := bytes.NewBuffer([]byte("abc"))
	content, _ := io.ReadAll(r)
	fmt.Println(content)
}
