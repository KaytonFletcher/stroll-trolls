//
//  ContentView.swift
//  stroll-trolls
//
//  Created by Kayton Fletcher on 10/26/19.
//  Copyright © 2019 Kayton Fletcher. All rights reserved.
//

import SwiftUI

struct ContentView: View {
    @State private var selection = 0
 
    var body: some View {
        
        TabView(selection: $selection){
            
            HomeView()
                .font(.title)
                .tabItem {
                    VStack {
                        Image("first")
                        Text("Home")
                    }
                }
                .tag(0)
            
            RouteHistoryView()
                .font(.title)
                .tabItem {
                    VStack {
                        Image("second")
                        Text("Previous Routes")
                    }
                }
                .tag(1)
        }
    }
}

struct ContentView_Previews: PreviewProvider {
    static var previews: some View {
        ContentView()
    }
}
