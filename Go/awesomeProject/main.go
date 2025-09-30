package main

import "awesomeProject/mylib"

func main() {
	mylib.HttpTest()
	// err := godotenv.Load()
	// if err != nil {
	// 	log.Fatalf("Error loading .env file")
	// }
	// token := os.Getenv("TIINGO_API_TOKEN")
	// spy, _ := quote.NewQuoteFromTiingo("spy", "2025-01-01", "2025-04-01", token)
	// fmt.Print(spy.CSV())
	// rsi2 := talib.Rsi(spy.Close, 2)
	// fmt.Println(rsi2)
}
