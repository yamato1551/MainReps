package main

import (
	"fmt"
	"log"
	"os"

	"github.com/joho/godotenv"

	"github.com/markcheno/go-quote"
	"github.com/markcheno/go-talib"
)

func main() {
	err := godotenv.Load()
	if err != nil {
		log.Fatalf("Error loading .env file")
	}
	token := os.Getenv("TIINGO_API_TOKEN")
	spy, _ := quote.NewQuoteFromTiingo("spy", "2025-01-01", "2025-04-01", token)
	fmt.Print(spy.CSV())
	rsi2 := talib.Rsi(spy.Close, 2)
	fmt.Println(rsi2)
}
