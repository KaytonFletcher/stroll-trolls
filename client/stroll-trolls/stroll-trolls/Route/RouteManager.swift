//
//  RouteManager.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/27/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import Foundation
import SwiftUI
import Combine

class RouteManager: ObservableObject {
    let routeUrl = "https://128.61.97.214:5001/api/MapBox"
    @Published var data: [RouteData]?
            
    func fetchRoute(){
       //let urlString = "\(routeUrl)/?distance=6&coordinates=60&coordinates=55&coordinates=61&coordinates=55"
        let urlString = "http://128.61.97.214:5001/api/MapBox/?distance=6&coordinates=-84.384932&coordinates=33.772388&coordinates=-84.388816&coordinates=33.772397"
        //let urlString = "http://localhost:3000"
        performRequest(with: urlString)
    }
    
    func performRequest(with urlString: String){
        
        if let url = URL(string: urlString){
        
            let session = URLSession(configuration: .default)
            
             print("YAS FUCK")
            
            let task = session.dataTask(with: url) { (data, response, error) in
                if error != nil {
                           print("NOT GOOD FAM")
                           print(error!)
                           return
                       }
                       
                       if let safeData = data {
                        self.parseJSON(routeData: safeData)
                           
                }
            }
            
            task.resume()
        }
    }
        
    
    func parseJSON(routeData: Data) {
        let decoder = JSONDecoder()
        do {
            let decodedData = try decoder.decode([RouteData].self, from: routeData)
            print(decodedData)
            self.data = decodedData
            
        } catch {
            print(error)
        }

    }
}
