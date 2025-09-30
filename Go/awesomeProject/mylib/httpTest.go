package mylib

import (
	"fmt"
	"io"
	"net/http"
	"net/url"
)

/**
 * HTTP接続
 */
func HttpTest() {
	// HTTP GET
	// resp, _ := http.Get("https://www.google.com")
	// defer resp.Body.Close()
	// body, _ := io.ReadAll(resp.Body)
	// fmt.Println(string(body))

	// URL生成
	base, _ := url.Parse("https://www.google.com/sea")
	reference, _ := url.Parse("/test?a=1&b=2")
	endpoint := base.ResolveReference(reference).String()
	fmt.Println(endpoint)
	req, _ := http.NewRequest("GET", endpoint, nil)
	req.Header.Add("User-Agent", "MyApp/1.0")
	q := req.URL.Query()
	q.Add("c", "3&%")
	fmt.Println(q)
	fmt.Println(q.Encode())
	req.URL.RawQuery = q.Encode()

	var client *http.Client = &http.Client{}
	resp, _ := client.Do(req)
	body, _ := io.ReadAll(resp.Body)
	fmt.Println(string(body))
}
